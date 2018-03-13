import ('System')
import ('Assignment')
import ('Assignment.Movement')
import ('Assignment.Entity')
import ('Assignment.World')

function enter(entity, world)
	entity.SteeringBehaviours:Add(ObstacleAvoidance())
	entity.SteeringBehaviours:Add(Wander())
end

function execute(entity, world)
	entity.Strength = entity.Strength + 1

	if entity.Strength > 100 then
		return "patrol"
	end

	return "hide"
end 

function exit(entity, world)
	entity.SteeringBehaviours:Clear()
end