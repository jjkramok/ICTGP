import ('System')
import ('Assignment')
import ('Assignment.Movement')
import ('Assignment.Entity')
import ('Assignment.World')

function enter(entity, world)

end

function execute(entity, world)
	entity.QuickEnergy = entity.QuickEnergy + 2
	entity.Food = entity.Food + 10
	entity.SlowEnergy = entity.SlowEnergy - 0.5

	local nearEntities = world:EntitiesInArea(entity.Location, 80)
	for i=0, nearEntities.Count - 1 do 
		if nearEntities[i].Type == EntityType.Omnivore then
			return "flee"
		end
	end

	if entity.Food > 200 then
		return "wander"
	end

	if entity.SlowEnergy < 10 then
		return "sleep"
	end

	return "eating"
end 

function exit(entity, world)
	entity:RemoveAllBehaviours()
end