import ('System')
import ('Assignment')
import ('Assignment.Movement')
import ('Assignment.Entity')
import ('Assignment.World')

function enter(entity, world)
	local Goal = Location(400, 500)
	local path = PathFollowing(Goal)

	entity:AddBehaviour(ObstacleAvoidance())
	entity:AddBehaviour(path)
end

function execute(entity, world)
	entity.SlowEnergy = entity.SlowEnergy - 1

	if entity.SlowEnergy < 50 then
		return "hide"
	end

	return "attack"
end 

function exit(entity, world)
	entity:RemoveAllBehaviours()
end