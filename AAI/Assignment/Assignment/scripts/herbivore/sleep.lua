import ('System')
import ('Assignment')
import ('Assignment.Movement')
import ('Assignment.Entity')
import ('Assignment.World')

function enter(entity, world)
	
end

function execute(entity, world)
	entity.QuickEnergy = entity.QuickEnergy + 0.01
	entity.SlowEnergy = entity.SlowEnergy + 5
	entity.Food = entity.Food - 0.1

	local nearEntities = world:EntitiesInArea(entity.Location, 40)
	for i=0, nearEntities.Count - 1 do 
		if nearEntities[i].Type == EntityType.Omnivore then
			return "flee"
		end
	end

	if entity.SlowEnergy > 150 then
		return "wander"
	end

	return "sleep"
end 

function exit(entity, world)
	entity:RemoveAllBehaviours()
end