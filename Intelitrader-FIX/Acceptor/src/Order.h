#pragma once

#include "quickfix/Fields.h"

class Order
{
public:
	std::string ClOrdID;
	std::string Symbol;
	std::string Side;
	std::string TransactTime;
	std::string OrderStatus;
	std::string OrderQty;
	std::string OrdType;
	Order() {};
	Order(std::string ClOrdID,
	std::string Symbol,
	std::string Side,
	std::string TransactTime,
	std::string OrderQty,
	std::string OrdType);
};

