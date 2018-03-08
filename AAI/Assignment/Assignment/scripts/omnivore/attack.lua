import ('System')
import ('Assignment')
import ('Assignment.Movement')
import ('Assignment.Entity')
import ('Assignment.World')

function enter(entity, world)
	local arrive = Arrive()
	arrive.ArriveLocation = Location(100, 100)

	entity.SteeringBehaviours:Add(arrive)
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