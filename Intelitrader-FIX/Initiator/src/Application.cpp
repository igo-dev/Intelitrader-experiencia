#ifdef _MSC_VER
#pragma warning( disable : 4503 4355 4786 )
#else
#include "config.h"
#endif

#include "Application.h"
#include "quickfix/Session.h"
#include <iostream>

void Application::fromAdmin(const FIX::Message& message, const FIX::SessionID& sessionID) 
{
    crack(message, sessionID);
}

void Application::fromApp(const FIX::Message& message, const FIX::SessionID& sessionID)
throw(FIX::FieldNotFound, FIX::IncorrectDataFormat, FIX::IncorrectTagValue, FIX::UnsupportedMessageType)
{
    crack(message, sessionID);
}

void Application::toApp(FIX::Message& message, const FIX::SessionID& sessionID)
throw(FIX::DoNotSend)
{
    try
    {
        FIX::PossDupFlag possDupFlag;
        message.getHeader().getField(possDupFlag);
        if (possDupFlag) throw FIX::DoNotSend();
    }
    catch (FIX::FieldNotFound&) {}
}

void Application::onMessage
(const FIX44::ExecutionReport& message, const FIX::SessionID&) 
{
    std::string ordID = message.getField(FIX::FIELD::OrderID);
    std::string ordStatus;

    switch (message.getField(FIX::FIELD::OrdStatus)[0])
    {
    case '0':
        ordStatus = "NEW";
        break;
    case '4':
        ordStatus = "CANCELED";
        break;
    }

    std::cout 
        << "\nOrderID = " << ordID << " Status = " << ordStatus << std::endl;
}

void Application::onMessage
(const FIX44::BusinessMessageReject& message, const FIX::SessionID&)
{
    std::cout << "\n" << message.getField(FIX::FIELD::Text) << std::endl;
}

void Application::onMessage
(const FIX44::Reject& message, const FIX::SessionID&)
{
        std::cout
            << "Message Rejected!" << std::endl
            << message.getField(58) << ": " << message.getField(371) << std::endl
            << "\n";
}

void Application::run()
{
    char userInput;

    while (true)
    {
        std::cout 
            << "\nStock Exchange Simulator" << std::endl 
            << "\nMy Orders\n" << std::endl
            << "1. Send new order" << std::endl
            << "2. Order Status" << std::endl
            << "3. Cancel Order" << std::endl
            << "0. Exit\n" << std::endl;

        std::cin >> userInput;

        switch(userInput)
        {
        case '1': 
            createNewOrderSingle();
            break;
        case '2':
            createOrderStatusRequest();
            break;
        case '3':
            createOrderCancelRequest();
            break;
        case '0':
            std::cout << "Bye!" << std::endl;
            return;
        default:
            std::cout << "Invalid option!" << std::endl;
            break;
        }
    }
}

FIX::Side Application::askSide()
{
    std::cout
        << "\nSide of order:\n" << std::endl
        << "1. Buy" << std::endl
        << "2. Sell" << std::endl;

    char value;
    std::cin >> value;

    return FIX::Side(value);
}

FIX::ClOrdID Application::askClOrdID()
{
    std::cout
        << "\nOrder Id:\n" << std::endl;

    std::string value;
    std::cin >> value;

    return FIX::ClOrdID(value);
}

FIX::OrderQty Application::askOrderQty()
{
    std::cout
        << "\nQuantity:\n" << std::endl;

    std::int32_t value;
    std::cin >> value;

    return FIX::OrderQty(value);
}

FIX::OrdType Application::askOrdType()
{
    std::cout
        << "\nType:\n" << std::endl
        << "1. Market" << std::endl
        << "2. Limit" << std::endl
        << "3. Stop Loss" << std::endl
        << "4. Stop Limit" << std::endl
        << "K. Market With Leftover As Limit" << std::endl
        << "W. RLP" << std::endl;

    char value;
    std::cin >> value;

    return FIX::OrdType(value);
}

FIX::Symbol Application::askSymbol()
{
    std::cout
        << "\nSymbol:\n" << std::endl;

    std::string value;
    std::cin >> value;

    return FIX::Symbol(value);
}

void Application::createNewOrderSingle()
{
    FIX::Message message;

    message.getHeader().setField(FIX::BeginString("FIX.4.4"));
    message.getHeader().setField(FIX::MsgType("D"));
    message.getHeader().setField(FIX::SenderCompID("FIXCLIENT1"));
    message.getHeader().setField(FIX::TargetCompID("FIXACCEPTOR"));
    message.getHeader().setField(FIX::SendingTime());
    
    message.setField(askClOrdID());
    message.setField(FIX::NoPartyIDs(0));
    message.setField(askSide());
    message.setField(FIX::TransactTime());
    message.setField(askOrdType());
    message.setField(askOrderQty());
  
    message.setField(askSymbol());

    std::cout
        << "\nMessage sent:" << std::endl
        << message << std::endl;

    FIX::Session::sendToTarget(message);
}

void Application::createOrderStatusRequest()
{
    FIX::Message message;

    message.getHeader().setField(FIX::BeginString("FIX.4.4"));
    message.getHeader().setField(FIX::MsgType(FIX::MsgType_OrderStatusRequest));
    message.getHeader().setField(FIX::SenderCompID("FIXCLIENT1"));
    message.getHeader().setField(FIX::SendingTime());
    message.getHeader().setField(FIX::TargetCompID("FIXACCEPTOR"));

    message.setField(FIX::OrderID(askClOrdID()));

    std::cout
        << "\nMessage sent:" << std::endl
        << message << std::endl;

    FIX::Session::sendToTarget(message);
}

void Application::createOrderCancelRequest()
{
    FIX::Message message;

    message.getHeader().setField(FIX::BeginString("FIX.4.4"));
    message.getHeader().setField(FIX::MsgType(FIX::MsgType_OrderCancelRequest));
    message.getHeader().setField(FIX::SenderCompID("FIXCLIENT1"));
    message.getHeader().setField(FIX::SendingTime());
    message.getHeader().setField(FIX::TargetCompID("FIXACCEPTOR"));

    std::string tmpClOrdID = askClOrdID();

    message.setField(FIX::OrigClOrdID(tmpClOrdID));
    message.setField(FIX::ClOrdID(tmpClOrdID));
    message.setField(FIX::Symbol("0"));
    message.setField(FIX::Side('1'));
    message.setField(FIX::OrderQty(0));
    message.setField(FIX::TransactTime());
    message.setField(FIX::NoPartyIDs(0));

    std::cout
        << "\nMessage sent:" << std::endl
        << message << std::endl;

    FIX::Session::sendToTarget(message);
}
