import ('System')
import ('Assignment')
import ('Assignment.Movement')
import ('Assignment.Entity')

state = "wander"

added = 0

function wander()
	if added == 0 then
		entity.SteeringBehaviours:Add(ObstacleAvoidance());

		local flee = Flee()
		flee.Radius = 200
		flee.FleeFrom = EntityType.Omnivore

		entity.SteeringBehaviours:Add(flee);
		entity.SteeringBehaviours:Add(Wander());
		added = 1
	end
end
