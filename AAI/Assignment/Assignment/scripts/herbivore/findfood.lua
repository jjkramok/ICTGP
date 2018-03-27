import ('System')
import ('Assignment')
import ('Assignment.Movement')
import ('Assignment.Entity')
import ('Assignment.World')
import ('Assignment.Utilities')

function enter(entity, world)
	local size = 50
	local trees = world:FoodInArea(entity.Location, size)
	while trees.Count == 0 and size < 5000 do
		size = size + 50
		trees = world:FoodInArea(entity.Location, size)
	end

	if trees.Count > 0 then
		local Location goal = trees[0]
		entity:AddBehaviour(PathFollowing(Goal))
	else
		entity:AddBehaviour(Wander())
	end
	entity:AddBehaviour(ObstacleAvoidance())
end

function execute(entity, world)
	entity.SlowEnergy = entity.SlowEnergy - 1
	entity.QuickEnergy = entity.QuickEnergy - 0.001
	entity.Food = entity.Food - 0.3

	local nearEntities = world:EntitiesInArea(entity.Location, 100)
	for i=0, nearEntities.Count - 1 do 
		if nearEntities[i].Type == EntityType.Omnivore then
			return "flee"
		end
	end

	local behaviour = entity:GetBehaviourByType("PathFollowing")
	if behaviour == NULL then
		return "eating"
	end

	return "findfood"
end 

function exit(entity, world)
	entity:RemoveAllBehaviours()
end