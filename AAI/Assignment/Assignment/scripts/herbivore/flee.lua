import ('System')
import ('Assignment')
import ('Assignment.Movement')
import ('Assignment.Entity')
import ('Assignment.World')

function enter(entity, world)
	entity:AddBehaviour(ObstacleAvoidance())

	local flee = Flee()
	flee.FleeFrom = EntityType.Omnivore
	flee.Radius= 200

	entity:AddBehaviour(flee)
end

function execute(entity, world)
	entity.QuickEnergy = entity.QuickEnergy - 5
	entity.SlowEnergy = entity.SlowEnergy - 2	
	entity.Food = entity.Food - 2


	local nearEntities = world:EntitiesInArea(entity.Location, 200)
	for i=0, nearEntities.Count - 1 do 
		if nearEntities[i].Type == EntityType.Omnivore then
			return "flee"
		end
	end

	return "wander"
end 

function exit(entity, world)
	entity:RemoveAllBehaviours()
end