import ('System')
import ('Assignment')
import ('Assignment.Movement')
import ('Assignment.Entity')
import ('Assignment.World')

function enter(entity, world)
	local chaseEntities = world:EntitiesInArea(entity.Location, 100.0, EntityType.Herbivore)
	if chaseEntities.Count > 0 then
		local seek = Seek()
		seek.ChaseEntity = chaseEntities[0]
		seek.MaxDistance = 150
		entity:AddBehaviour(seek)

	end
	entity:AddBehaviour(ObstacleAvoidance())
end

function execute(entity, world)
	entity.QuickEnergy = entity.QuickEnergy - 1

	local behaviour = entity:GetBehaviourByType("Seek")
	if behaviour == NULL then
		local chaseEntities = world:EntitiesInArea(entity.Location, 10.0, EntityType.Herbivore)
		if chaseEntities.Count > 0 then
			return "eat"
		else
			return "patrol"
		end
	end

	return "attack"
end 

function exit(entity, world)
	entity:RemoveAllBehaviours()
end