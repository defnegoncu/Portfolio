#pragma once
#include "CKnowladge.h"
#include "Vektoria\Root.h"

using namespace Vektoria;
class CKnowladgePosition :
    public CKnowladge
{
public:
    CKnowladgePosition();
    ~CKnowladgePosition();
    CHVector* m_position;
    void Init(CHVector* a_position);
};

