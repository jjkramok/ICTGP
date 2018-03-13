import ('System')
import ('Assignment')
import ('Assignment.Movement')
import ('Assignment.Entity')
import ('Assignment.World')

function enter(entity, world)
	entity:AddBehaviour(ObstacleAvoidance())
	entity:AddBehaviour(Wander())
end

function execute(entity, world)
	entity.SlowEnergy = entity.SlowEnergy + 1

	if entity.SlowEnergy > 100 then
		return "attack"
	end

	return "patrol"
end 

function exit(entity, world)
	entity:RemoveAllBehaviours()
end