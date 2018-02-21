#ifndef STEERING_H
#define STEERING_H

#include "../Entity/Entity.h"
#include "SteeringForce.h"
#include <stdlib.h>
#include <vector>

class Steering
{
public:
	Steering();
	virtual ~Steering();

	SteeringForce flee(Entity* entity);
	SteeringForce flee(int entityType);

	SteeringForce seek(Entity* entity);
	SteeringForce seek(int entityType);

	SteeringForce wander();
	SteeringForce arrive(std::vector<float> location);
	SteeringForce arrive(Entity* entity);

	SteeringForce follow(Entity* entity);

	SteeringForce flocking(int entityType);

	SteeringForce obstacleAvoidence();

	SteeringForce hide(int entityType);



};



#endif