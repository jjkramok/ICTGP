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
	entity.QuickEnergy = entity.QuickEnergy + 1
	
	if entity.QuickEnergy > 100 then
		return "attack"
	end

	return "patrol"
end 

function exit(entity, world)
	entity:RemoveAllBehaviours()
end