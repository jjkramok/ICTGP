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
addLamppost(5, 0, 5, 3, 0.5, scene);
addTree(10, 0, 15, scene);
addTerrain(1000, 1000, scene);

addSkybox(1000, scene);

// Add axis helper TODO remove?
var worldAxis = new THREE.AxisHelper(500);
scene.add(worldAxis);

/* Camera and Light */
// TODO remove; light used for debugging
var ambientLight = new THREE.AmbientLight(0x404040);
scene.add(ambientLight);

var light = new THREE.DirectionalLight(0xdddddd, 1.0);
light.position.set(600, 200, 1000);
scene.add(light);

// Move camera from center
camera.position.set(30, 10, 50);
controls.update();

// Animations and stuff
var clock = new THREE.Clock();

var render = function () {
    requestAnimationFrame(render);
    var delta = clock.getDelta();

    controls.update();

    renderer.render(scene, camera);
};

render();