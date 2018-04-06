// GenAddress.h
// Генерирование адресов

#ifndef __GEN_ADDRESS_H__
#define __GEN_ADDRESS_H__

#include "DinTypes.h"

char *GenAddressHome(const IN_DATA_ADDRESS *in_data);
char *GenHumanAddress(const IN_DATA_ADDRESS *in_data);
char *GenCompanyAddress(const IN_DATA_ADDRESS *in_data);

#endif