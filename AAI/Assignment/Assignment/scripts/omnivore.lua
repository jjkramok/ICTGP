import ('System')
import ('Assignment')
import ('Assignment.Movement')

state = "wander"

added = 0

function wander()
	if added == 0 then
		entity.SteeringBehaviours:Add(Wander())
		entity.SteeringBehaviours:Add(ObstacleAvoidance())
		added = 1
	end
end
