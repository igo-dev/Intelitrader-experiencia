#include "Application.h"

std::map<std::string, Order> Orders;

void Application::onLogon(const FIX::SessionID& sessionID)
{
	std::cout << std::endl << "Logon - " << sessionID << std::endl;
}

void Application::onLogout(const FIX::SessionID& sessionID)
{
	std::cout << std::endl << "Logout - " << sessionID << std::endl;
}

void Application::toAdmin(FIX::Message& message, const FIX::SessionID&)
{
	std::cout << std::endl << "ADMIN OUT: " << message << std::endl;
}

void Application::fromAdmin(const FIX::Message& message, const FIX::SessionID&)
throw(FIX::FieldNotFound, FIX::IncorrectDataFormat, FIX::IncorrectTagValue, FIX::RejectLogon)
{
	std::cout << std::endl << "ADMIN IN: " << message << std::endl;
}
void Application::fromApp(const FIX::Message& message, const FIX::SessionID& sessionID)
throw(FIX::FieldNotFound, FIX::IncorrectDataFormat, FIX::IncorrectTagValue, FIX::UnsupportedMessageType)
{
	crack(message, sessionID);
	std::cout << std::endl << "IN: " << message << std::endl;
}

void Application::toApp(FIX::Message& message, const FIX::SessionID& sessionID)
throw(FIX::DoNotSend)
{
	std::cout << std::endl << "OUT: " << message << std::endl;
}

// MESSAGE EVENT HANDLERS //

void Application::onMessage(const FIX44::NewOrderSingle& message, const FIX::SessionID&)
{
	if (Orders.count(message.getField(FIX::FIELD::ClOrdID)))
	{
		FIX::Session::sendToTarget(sendReject(message, "ORDER ALREADY IN USE!"));
		return;
	}

	std::string clOrdID = message.getField(FIX::FIELD::ClOrdID);
	std::string symbol = message.getField(FIX::FIELD::Symbol);
	std::string side = message.getField(FIX::FIELD::Side);
	std::string transactTime = message.getField(FIX::FIELD::TransactTime);
	std::string orderQty = message.getField(FIX::FIELD::OrderQty);
	std::string ordType = message.getField(FIX::FIELD::OrdType);

	Orders.insert(std::pair<std::string, Order>(clOrdID, Order(clOrdID, symbol, side, transactTime, orderQty, ordType)));

	FIX::Message execReport;

	execReport.getHeader().setField(FIX::BeginString("FIX.4.4"));
	execReport.getHeader().setField(FIX::MsgType("8"));
	execReport.getHeader().setField(FIX::SenderCompID("FIXACCEPTOR"));
	execReport.getHeader().setField(FIX::TargetCompID("FIXCLIENT1"));
	execReport.getHeader().setField(FIX::SendingTime());

	execReport.setField(FIX::FIELD::OrderID, clOrdID);
	execReport.setField(FIX::FIELD::NoPartyIDs, "0");
	execReport.setField(FIX::FIELD::ExecID, clOrdID);
	execReport.setField(FIX::FIELD::ExecType, "0");
	execReport.setField(FIX::FIELD::OrdStatus, "0");
	execReport.setField(FIX::FIELD::Symbol, symbol);
	execReport.setField(FIX::FIELD::Side, side);
	execReport.setField(FIX::FIELD::OrderQty, orderQty);
	execReport.setField(FIX::FIELD::LeavesQty, orderQty);
	execReport.setField(FIX::FIELD::CumQty, "0");
	execReport.setField(FIX::FIELD::AvgPx, "0");
	execReport.setField(FIX::TransactTime());
	execReport.setField(FIX::FIELD::Side, side);
	execReport.setField(FIX::Text("ORDER CREATED!"));

	FIX::Session::sendToTarget(execReport);

	std::cout << "\nNEW ORDER RECIEVED" << std::endl;
}

void Application::onMessage(const FIX44::OrderCancelRequest& message, const FIX::SessionID& sessionID)
{

	if (Orders.count(message.getField(FIX::FIELD::ClOrdID)))
	{
		Orders[message.getField(FIX::FIELD::ClOrdID)].OrderStatus = "4";

		FIX::Message execReport;

		execReport.getHeader().setField(FIX::BeginString("FIX.4.4"));
		execReport.getHeader().setField(FIX::MsgType("8"));
		execReport.getHeader().setField(FIX::SenderCompID("FIXACCEPTOR"));
		execReport.getHeader().setField(FIX::TargetCompID("FIXCLIENT1"));
		execReport.getHeader().setField(FIX::SendingTime());

		execReport.setField(FIX::FIELD::OrderID, message.getField(FIX::FIELD::ClOrdID));
		execReport.setField(FIX::FIELD::NoPartyIDs, message.getField(FIX::FIELD::NoPartyIDs));
		execReport.setField(FIX::FIELD::ExecID, message.getField(FIX::FIELD::ClOrdID));
		execReport.setField(FIX::FIELD::ExecType, "4");
		execReport.setField(FIX::FIELD::OrdStatus, "4");
		execReport.setField(FIX::FIELD::Symbol, message.getField(FIX::FIELD::Symbol));
		execReport.setField(FIX::FIELD::Side, message.getField(FIX::FIELD::Side));
		execReport.setField(FIX::FIELD::OrderQty, message.getField(FIX::FIELD::OrderQty));
		execReport.setField(FIX::FIELD::LeavesQty, "0");
		execReport.setField(FIX::FIELD::CumQty, "0");
		execReport.setField(FIX::FIELD::AvgPx, "0");
		execReport.setField(FIX::TransactTime());
		execReport.setField(FIX::Text("ORDER CANCELED!"));

		FIX::Session::sendToTarget(execReport);

		std::cout << "\nORDER CANCELED" << std::endl;
	}
	else
	{
		FIX::Session::sendToTarget(sendReject(message, "ORDER CANCEL REJECTED!"));

		std::cout << "\nORDER CANCEL REJECTED" << std::endl;
	}
}

void Application::onMessage(const FIX44::OrderStatusRequest& message, const FIX::SessionID&)
{
	bool isIdValid = Orders.count(message.getField(FIX::FIELD::OrderID));

	if (isIdValid)
	{
		Order order = Orders[message.getField(FIX::FIELD::OrderID)];

		FIX::Message execReport;

		execReport.getHeader().setField(FIX::BeginString("FIX.4.4"));
		execReport.getHeader().setField(FIX::MsgType("8"));
		execReport.getHeader().setField(FIX::SenderCompID("FIXACCEPTOR"));
		execReport.getHeader().setField(FIX::TargetCompID("FIXCLIENT1"));
		execReport.getHeader().setField(FIX::SendingTime());

		execReport.setField(FIX::FIELD::OrderID, order.ClOrdID);
		execReport.setField(FIX::FIELD::NoPartyIDs, "0");
		execReport.setField(FIX::FIELD::ExecID, order.ClOrdID);
		execReport.setField(FIX::FIELD::ExecType, order.OrderStatus);
		execReport.setField(FIX::FIELD::OrdStatus, order.OrderStatus);
		execReport.setField(FIX::FIELD::Symbol, order.Symbol);
		execReport.setField(FIX::FIELD::Side, order.Side);
		execReport.setField(FIX::FIELD::OrderQty, order.OrderQty);
		execReport.setField(FIX::FIELD::LeavesQty, "0");
		execReport.setField(FIX::FIELD::CumQty, "0");
		execReport.setField(FIX::FIELD::AvgPx, "0");
		execReport.setField(FIX::TransactTime());

		FIX::Session::sendToTarget(execReport);

		std::cout << "\nORDER STATUS SENT" << std::endl;
	}
	else
	{
		FIX::Session::sendToTarget(Application::sendReject(message, "ORDER STATUS REJECTED!"));

		std::cout << "\nORDER STATUS REJECTED!" << std::endl;
	}
}

FIX::Message Application::sendReject(FIX::Message message, std::string text) 
{
	FIX44::BusinessMessageReject businessMessageReject
	(
		FIX::RefMsgType(message.getHeader().getField(FIX::FIELD::MsgType)),
		FIX::BusinessRejectReason(FIX::BusinessRejectReason_UNKOWN_ID)
	);

	businessMessageReject.getHeader().setField(FIX::SenderCompID("FIXACCEPTOR"));
	businessMessageReject.getHeader().setField(FIX::TargetCompID("FIXCLIENT1"));
	businessMessageReject.getHeader().setField(FIX::SendingTime());
	businessMessageReject.setField(FIX::Text(text));

	return businessMessageReject;
}