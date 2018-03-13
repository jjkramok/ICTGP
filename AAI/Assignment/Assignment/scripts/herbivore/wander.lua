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
	entity.SlowEnergy = entity.SlowEnergy - 1
	entity.QuickEnergy = entity.QuickEnergy + 1
	entity.Food = entity.Food - 1 

	local nearEntities = world:EntitiesInArea(entity.Location, 150)
	for i=0, nearEntities.Count - 1 do 
		if nearEntities[i].Type == EntityType.Omnivore then
			return "flee"
		end
	end

	if entity.SlowEnergy < 10 then
		return "sleep"
	end

	if entity.Food < 30 then
		return "findfood"
	end

	return "wander"
end 

function exit(entity, world)
	entity:RemoveAllBehaviours()
end