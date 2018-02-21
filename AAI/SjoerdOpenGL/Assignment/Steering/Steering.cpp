#include "Steering.h"


Steering::Steering()
{
}


Steering::~Steering()
{
}

SteeringForce Steering::flee(Entity * entity)
{
	entity->location;
	SteeringForce a = {3.0f};
	return a;
}

SteeringForce Steering::flee(int entityType)
{
	return SteeringForce();
}

SteeringForce Steering::seek(Entity * entity)
{
	return SteeringForce();
}

SteeringForce Steering::seek(int entityType)
{
	return SteeringForce();
}

SteeringForce Steering::wander()
{
	return SteeringForce();
}

SteeringForce Steering::arrive(std::vector<float> location)
{
	return SteeringForce();
}

SteeringForce Steering::arrive(Entity * entity)
{
	return SteeringForce();
}

SteeringForce Steering::follow(Entity * entity)
{
	return SteeringForce();
}

SteeringForce Steering::flocking(int entityType)
{
	return SteeringForce();
}

SteeringForce Steering::obstacleAvoidence()
{
	return SteeringForce();
}

SteeringForce Steering::hide(int entityType)
{
	return SteeringForce();
}
