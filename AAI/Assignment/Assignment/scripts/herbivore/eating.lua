import ('System')
import ('Assignment')
import ('Assignment.Movement')
import ('Assignment.Entity')
import ('Assignment.World')

function enter(entity, world)

end

function execute(entity, world)
	local tree = findtree(entity, world)
	if tree == 0 then
		return "wander"
	end

	local food = tree:EatFood(10)
	if food < 1 then
		return "wander"
	end 

	entity.QuickEnergy = entity.QuickEnergy + 0.02
	entity.Food = entity.Food + food
	entity.SlowEnergy = entity.SlowEnergy - 0.5

	local nearEntities = world:EntitiesInArea(entity.Location, 30)
	for i = 0, nearEntities.Count - 1 do 
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


function findtree(entity, world)
	local size = 10
	local trees = world:FoodInArea(entity.Location, size)
	while trees.Count == 0 and size < 100 do
		size = size + 10
		trees = world:FoodInArea(entity.Location, size)
	end
	if trees.Count == 0 then 
		return 0
	end
	return trees[0]
end