// Create scene
var scene = new THREE.Scene();

// Create camera
var camera = new THREE.PerspectiveCamera(
	75, // fov — Camera frustum vertical field of view.
	window.innerWidth/window.innerHeight, // aspect — Camera frustum aspect ratio.
	0.1, // near — Camera frustum near plane.
	10000); // far — Camera frustum far plane.

var controls = new THREE.OrbitControls( camera );

// Create renderer
var renderer = new THREE.WebGLRenderer();
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

// Populate scene with meshes
addTerrain(1000, 1000, scene);
addLamppost(3, 0, 15, 3, 0.5, scene);
addLamppost(15, 0, 1.3, 3, 0.5, scene);
addTree(1.5, 0, 1, scene);
addTree(13, 0, 12, scene);

CreateHouse(0, 0, 5, scene);
CreateHouse(12, 0, 2, scene);
CreateHouse(12, 0, 17, scene);

var car = CreateCar(-3, 0, -1, scene);

addSkybox(1000, scene);


var sun = new THREE.PointLight( 0xf9d71c, 1 );
sun.position.set( 200, 500, 200 );
var sphere = new THREE.SphereGeometry(5.5, 16, 8);
sun.add(new THREE.Mesh(sphere, new THREE.MeshBasicMaterial({ color: 0xf9d71c})));

scene.add( sun );

// Move camera from center
camera.position.set(30, 10, 50);
controls.update();

// Animations and stuff
var clock = new THREE.Clock();

var render = function () {
    requestAnimationFrame(render);
    var delta = clock.getDelta();

    DriveCar(car, delta);

    controls.update();

    renderer.render(scene, camera);
};
setTimeout(function () {
    render();
}, 500);