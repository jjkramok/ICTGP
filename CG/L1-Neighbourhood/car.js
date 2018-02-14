function CreateCar(x, y, z, scene) {
    var car = { obj: null, state: 0, distance: 0 };


    // load a resource
    var loader = new THREE.ObjectLoader();
    loader.load(
        './ModelsAndTextures/car/police-car.json',

        // onLoad callback
        // Here the loaded data is assumed to be an object
        function (obj) {
            car.obj = obj;

            obj.position.x += x + 0.9;
            obj.position.y += y + 0.35;
            obj.position.z += z + 0.5;

            // set material properties.
            obj.children[0].material = new THREE.MeshStandardMaterial({ color: 0x00ffff });
            obj.children[1].material = new THREE.MeshNormalMaterial({ color: 0xff000 });
            obj.children[2].material = new THREE.MeshPhongMaterial({ color: 0x00ff00 });
            obj.children[3].material = new THREE.MeshPhysicalMaterial({ color: 0xffff00 });
            obj.children[5].material = new THREE.MeshNormalMaterial();

            /*
            for (var i = 0; i < 6; i++) {
                console.log("stuff", obj.children[i]);
                if (obj.children[i].geometry) {
                    obj.children[i].geometry.computeFaceNormals();
                    obj.children[i].geometry.uvsNeedUpdate = true;
                    obj.children[i].geometry.verticesNeedUpdate = true;
                    obj.children[i].geometry.elementsNeedUpdate = true;
                    obj.children[i].geometry.normalsNeedUpdate = true;
                }
            }
            */
            // add to scene
            scene.add(obj);
        },
        // onProgress callback
        function (xhr) {
            console.log((xhr.loaded / xhr.total * 100) + '% loaded');
        },

        // onError callback
        function (err) {
            console.error('An error happened');
        }
    );

    return car;
}

function DriveCar(car) {
    if (!car.obj)
        return;

    var delta = 1 / 60;

    var carspeed = 6;

    car.distance += 0.5 * delta;
    if (car.distance > 1) {
        car.distance = 1;
    }

    switch (car.state) {
        case 0: // drive one way
            car.obj.position.x += carspeed * delta;
            break;
        case 1: // rotate
            car.obj.rotation.y += (Math.PI * 2) * 0.125 * delta;
            car.obj.position.x += (1 - car.distance) * carspeed * delta;
            car.obj.position.z += -car.distance * carspeed * delta;
            break;
        case 2: // rotate
            car.obj.rotation.y += (Math.PI * 2) * 0.125 * delta;
            car.obj.position.x += -car.distance * carspeed * delta;
            car.obj.position.z += -(1 - car.distance) * carspeed * delta;
            break;
        case 3: // drive back 
            car.obj.position.x -= carspeed * delta;
            break;
        case 4: // rotate
            car.obj.rotation.y += (Math.PI * 2) * 0.125 * delta;
            car.obj.position.x += -(1 - car.distance) * carspeed * delta;
            car.obj.position.z += (car.distance) * carspeed * delta;
            break;
        case 5: // rotate
            car.obj.rotation.y += (Math.PI * 2) * 0.125 * delta;
            car.obj.position.x += (car.distance) * carspeed * delta;
            car.obj.position.z += (1 - car.distance) * carspeed * delta;
            break;
    }

    // update state.
    if (car.distance >= 1) {
        car.distance = 0;
        car.state++;
        if (car.state > 5) {
            car.state = 0;
        }
    }
}