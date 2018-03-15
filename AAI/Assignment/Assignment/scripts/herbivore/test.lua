import ('System')
import ('Assignment')
import ('Assignment.Movement')
import ('Assignment.Entity')
import ('Assignment.World')
import ('Assignment.Utilities')

function enter(entity, world)
end

function execute(entity, world)
	
	entity.Direction = entity.Direction + 0.01

	return "test"
end 

function exit(entity, world)
end