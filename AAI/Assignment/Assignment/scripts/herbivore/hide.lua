import ('System')
import ('Assignment')
import ('Assignment.Movement')
import ('Assignment.Entity')
import ('Assignment.World')

function enter(entity, world)
	local flee = Flee()
	flee.FleeFrom = EntityType.Omnivore
	flee.Radius = 200
	
	entity.SteeringBehaviours:Add(ObstacleAvoidance())
	entity.SteeringBehaviours:Add(flee)
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