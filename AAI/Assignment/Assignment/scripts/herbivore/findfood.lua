import ('System')
import ('Assignment')
import ('Assignment.Movement')
import ('Assignment.Entity')
import ('Assignment.World')
import ('Assignment.Utilities')
import ('Assignment.Fuzzy')

function enter(entity, world)
	local tree = FuzzyMachine.BestTree(entity)

	if tree == NULL then
		entity:AddBehaviour(Wander())
	else
		local path = PathFollowing(tree.Location)
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

	local behaviour = entity:GetBehaviourByType("PathFollowing")
	if behaviour == NULL then
		return "eating"
	end

	return "findfood"
end 

function exit(entity, world)
	entity:RemoveAllBehaviours()
end