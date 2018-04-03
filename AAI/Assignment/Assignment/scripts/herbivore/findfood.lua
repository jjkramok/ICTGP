import ('System')
import ('Assignment')
import ('Assignment.Movement')
import ('Assignment.Entity')
import ('Assignment.World')
import ('Assignment.Utilities')
import ('Assignment.Fuzzy')

function enter(entity, world)
	local tree = FuzzyMachine.BestTree(entity)

	if tree == null then
		Wander("")
		entity:AddBehaviour(Wander())
	else
		local path
		if Settings.Instance.UseTimeSlicedNavigation then
			path = PathFollowingTimeSliced(tree.Location)
		else
			path = PathFollowing(tree.Location)
		end
		path.SuccessDistance = 40
		entity:AddBehaviour(path)
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

	if entity:GetBehaviourByType("PathFollowing") == NULL and entity:GetBehaviourByType("PathFollowingTimeSliced") == NULL then
		return "eating"
	end

	return "findfood"
end 

function exit(entity, world)
	entity:RemoveAllBehaviours()
end
