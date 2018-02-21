#ifndef ENTITY_H
#define ENTITY_H

#include <stdlib.h>
#include <vector>

class Entity
{
public:
	Entity();
	~Entity();

	std::vector<float> location;
};


#endif