import ('System')
import ('Assignment')
import ('Assignment.Movement')
import ('Assignment.Entity')
import ('Assignment.World')

function enter(entity, world)
end

function execute(entity, world)
	entity.QuickEnergy = entity.QuickEnergy + 2

	if entity.QuickEnergy > 100 then
		return "patrol"
	end

	return "eat"
end 

function exit(entity, world)
	entity:RemoveAllBehaviours()
end