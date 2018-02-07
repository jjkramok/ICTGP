function CreateCar(x, y, z, scene) {
    var car = { obj: undefined, state: 0, distance: 0 };

    var loader = new THREE.ObjectLoader();
    // load a resource
    loader.load(
        // resource URL
        './ModelsAndTextures/car/police-car.json',

        // onLoad callback
        // Here the loaded data is assumed to be an object
        function (obj) {
            // Add the loaded object to the scene
            car.obj = obj;

            obj.position.x += x + 0.9;
            obj.position.y += y + 0.35;
            obj.position.z += z + 0.5;

            // set material properties.
            obj.children[0].material = new THREE.MeshLambertMaterial({ color: 0x00ffff });
            obj.children[1].material = new THREE.MeshNormalMaterial({ color: 0xff000 });
            obj.children[2].material = new THREE.MeshPhongMaterial({ color: 0x00ff00 });
            obj.children[3].material = new THREE.MeshPhysicalMaterial({ color: 0xffff00 });
            obj.children[4].material = new THREE.MeshStandardMaterial({ color: 0x0000ff });
            obj.children[5].material = new THREE.MeshNormalMaterial();


            scene.add(obj);
        },
        // onProgress callback
        function (err) {
            console.log((xhr.loaded / xhr.total * 100) + '% loaded');
        },

        // onError callback
        function (xhr) {
            console.error('An error happened');
        }
    );

    return car;
}

function DriveCar(car, delta) {
    var carspeed = 6;


    switch (car.state) {
        case 0: // drive one way
            car.obj.position.x += carspeed * delta;
            car.distance += 0.5 * delta;
            break;
        case 1: // rotate
            car.obj.rotation.y += (Math.PI * 2) * 0.125 * delta;
            car.obj.position.x += (1 - car.distance) * carspeed * delta;
            car.obj.position.z += -car.distance * carspeed * delta;
            car.distance += 0.5 * delta;
            break;
        case 2: // rotate
            car.obj.rotation.y += (Math.PI * 2) * 0.125 * delta;
            car.obj.position.x += -car.distance * carspeed * delta;
            car.obj.position.z += -(1 - car.distance) * carspeed * delta;
            car.distance += 0.5 * delta;
            break;
        case 3: // drive back 
            car.obj.position.x -= carspeed * delta;
            car.distance += 0.5 * delta;
            break;
        case 4: // rotate
            car.obj.rotation.y += (Math.PI * 2) * 0.125 * delta;
            car.obj.position.x += -(1 - car.distance) * carspeed * delta;
            car.obj.position.z += (car.distance) * carspeed * delta;
            car.distance += 0.5 * delta;
            break;
        case 5: // rotate
            car.obj.rotation.y += (Math.PI * 2) * 0.125 * delta;
            car.obj.position.x += (car.distance) * carspeed * delta;
            car.obj.position.z += (1 - car.distance) * carspeed * delta;
            car.distance += 0.5 * delta;
            break;
    }

    if (car.distance >= 1) {
        car.distance = 0;
        car.state++;
        if (car.state > 5) {
            car.state = 0;
        }
    }
}