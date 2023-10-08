<?php
// Header necessary to tell the server to use json
header('Content-Type: application/json');
try {
    // Database connection
	$database = mysqli_connect("localhost", "EoL_User", "A55ApzsgXRPS6gJb!", "camelote");
	$database->set_charset('utf8mb4');

    // User's id
    $id = $_POST['id'];

    // Get all the keys, they represent which resource we need to insert, the value is its new amount
    $keys = array_keys($_POST);

    // Loop on all the resources which need to be inserted 
    for($i = 1; $i < count($keys); $i++) {
        $resources_query = "INSERT INTO resources_to_user (user_id, resources, quantity) VALUES (?, ?, ?)";
		$stmt = $database->prepare($resources_query);
		$stmt->bind_param("iii", $id, $keys[$i], $_POST[$keys[$i]]);
		$stmt->execute();
    }

    // Close the database
	$database->close();
} catch (mysqli_sql_exception $e) { 
	// Errors in case the database connection failed
	echo "MySQLi Error Code: " . $e->getCode() . "<br />";
	echo "Exception Msg: " . $e->getMessage();
	exit(); 
}
?>