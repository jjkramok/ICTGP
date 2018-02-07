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

var geometry = new THREE.BoxBufferGeometry( 1, 1, 1 );
//var material = new THREE.MeshNormalMaterial();

var texture = new THREE.TextureLoader().load( "./ModelsAndTextures/Yellobrk.bmp" );
var geometry = new THREE.BoxBufferGeometry( 2, 2, 2 );
var material = new THREE.MeshBasicMaterial( { map: texture } );

/* Adding Meshes to the scene */
var cube = new THREE.Mesh(geometry, material);
scene.add(cube);

addLamppost(5, 0, 5, 3, 0.5, scene);
addTree(10, 0, 15, scene);

var worldAxis = new THREE.AxisHelper(500);
scene.add(worldAxis);

var skybox = createSkybox(1000);
scene.add(skybox);

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

var clock = new THREE.Clock();

var render = function () {
    requestAnimationFrame(render);
    var delta = clock.getDelta();

    cube.rotation.x += 1.2 * delta;
    cube.rotation.y += 1.2 * delta;
    cube.rotation.z += .2 * delta;

    controls.update();

    renderer.render(scene, camera);
};

render();