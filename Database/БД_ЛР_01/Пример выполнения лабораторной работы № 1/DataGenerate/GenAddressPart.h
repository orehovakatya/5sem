// GenAddressPart.h
// Генерировать часть адреса

#ifndef __GENERATE_ADDRESS_PART_H__
#define __GENERATE_ADDRESS_PART_H__

#include "DinTypes.h"

char *GenAddressPart1(const IN_DATA_ADDRESS_PART_1 *in_data);
char *GenAddressPart2(const IN_DATA_ADDRESS_PART_2 *in_data);
char *GenAddressHomePart(void);
char *GenAddressApartmentPart(void);
char *GenAddressOfficePart(void);

#endif