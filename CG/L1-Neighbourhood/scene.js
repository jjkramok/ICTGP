var scene, renderer, camera;
initScene();

CreateHouse(5, 0, 5, scene);
var car = CreateCar(0, 0, 0, scene);


var clock = new THREE.Clock();

var render = function () {
    requestAnimationFrame(render);
    var delta = clock.getDelta();
    DriveCar(car, delta);
    renderer.render(scene, camera);
};

function initScene() {
    // Create scene
    scene = new THREE.Scene();
    scene.background = new THREE.Color(100, 100, 100);

    // Create camera
    camera = new THREE.PerspectiveCamera(
        75, // fov — Camera frustum vertical field of view.
        window.innerWidth / window.innerHeight, // aspect — Camera frustum aspect ratio.
        0.1, // near — Camera frustum near plane.
        1000); // far — Camera frustum far plane. 

    var controls = new THREE.OrbitControls(camera);

    // Create renderer
    renderer = new THREE.WebGLRenderer();
    renderer.setSize(window.innerWidth, window.innerHeight);
    document.body.appendChild(renderer.domElement);

    // Move camera from center
    camera.position.x = 10;
    camera.position.y = 10;
    camera.position.z = 20;

    var light = new THREE.SpotLight(0xff00ff, 100, 100);
    light.position.set(50, 50, 50);
    scene.add(light);

    var axishelper = new THREE.AxisHelper(10);
    scene.add(axishelper);

    controls.update();

    return scene;
}

render();