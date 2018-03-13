import ('System')
import ('Assignment')
import ('Assignment.Movement')
import ('Assignment.Entity')
import ('Assignment.World')

function enter(entity, world)
	local path = PathFollowing()
	path.Goal = Location(400, 500)

	entity.SteeringBehaviours:Add(path)
end

function execute(entity, world)
	entity.Strength = entity.Strength - 1

	if entity.Strength < 50 then
		return "hide"
	end

	return "attack"
end 

function exit(entity, world)
	entity.SteeringBehaviours:Clear()
end