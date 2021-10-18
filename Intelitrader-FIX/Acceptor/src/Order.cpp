#include "Order.h"

Order::Order(std::string clOrdID, std::string symbol, std::string side, std::string transactTime, std::string orderQty, std::string ordType)
{
	ClOrdID = clOrdID;
	Symbol = symbol;
	Side = side;
	TransactTime = transactTime;
	OrderQty = orderQty;
	OrdType = ordType;
	OrderStatus = '0';
}
