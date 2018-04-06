// GenCompanyName.cpp
// ������������ ��� ��������

#include "GenCompanyName.h"
#include "FieldSizes.h"
#include "BestRandom.h"
#include "GenPartName.h"
#include <stdlib.h>
#include <stdio.h>
#include <string.h>

//------------------------------------------------------------------------------
char *GenComType(const IN_DATA_COMPANY *in_data);
//------------------------------------------------------------------------------
// ������������� ��� ��������
char *GenCompanyName(const IN_DATA_COMPANY *in_data)
{
	char *res, *com_type, *adj_noun_1, *adj_noun_2;
	res = (char*)calloc(SIZE_COMPANY_NAME, sizeof(char));
	com_type = GenComType(in_data);
	
	int type;  // (��, ���, ���, ���)
	
	type = RandInt(MAX_RND_NAME_TYPE);
	adj_noun_1 = GenAdjectNounPart(&(in_data->ComName), 
		type, SIZE_COMPANY_NAME);

	if(RandInt(2) == 0)  // ���� [<����>] <���> � [<����>] <���>
	{
		bool isRep = false;
		do  // ���� �� ��� ���, ���� ������ adj_noun_1 � adj_noun_2 ����� 
		{
			type = RandInt(MAX_RND_NAME_TYPE);
			adj_noun_2 = GenAdjectNounPart(&(in_data->ComName), 
				type, SIZE_COMPANY_NAME);
			isRep = (strcmp(adj_noun_1, adj_noun_2) == 0);

			if(!isRep)
			{
				_snprintf(res, SIZE_COMPANY_NAME-1,
					"%s \"%s � %s\"", com_type, adj_noun_1, adj_noun_2);
			}
			
			free(adj_noun_2);
		} while(isRep);
	}
	else  // ���� [<����>] <���>
	{
		_snprintf(res, SIZE_COMPANY_NAME-1,
			"%s \"%s\"", com_type, adj_noun_1);
	}	
	free(adj_noun_1);
	return res;
}

// ������������� ��� ��������
char *GenComType(const IN_DATA_COMPANY *in_data)
{
	char *res = GenStr(in_data->saComType);
	return res;
}

