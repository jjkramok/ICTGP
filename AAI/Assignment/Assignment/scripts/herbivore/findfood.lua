import ('System')
import ('Assignment')
import ('Assignment.Movement')
import ('Assignment.Entity')
import ('Assignment.World')
import ('Assignment.Utilities')

function enter(entity, world)
	local pathFollowing = PathFollowing()
	local trees = world.Ob

	pathFollowing.Goal = Location(500, 500)
	
	entity:AddBehaviour(pathFollowing)
	entity:AddBehaviour(ObstacleAvoidance())
end

function execute(entity, world)
	entity.SlowEnergy = entity.SlowEnergy - 1
	entity.QuickEnergy = entity.QuickEnergy + 0.01
	entity.Food = entity.Food - 0.5

	local nearEntities = world:EntitiesInArea(entity.Location, 100)
	for i=0, nearEntities.Count - 1 do 
		if nearEntities[i].Type == EntityType.Omnivore then
			return "flee"
		end
	end

	local behaviour = entity:GetBehaviourByType(PathFollowing())
	if behaviour == NULL then
		return "eating"
	end

	return "findfood"
end 

function exit(entity, world)
	entity:RemoveAllBehaviours()
end