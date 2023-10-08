<?php
// Header necessary to tell the server to use json
header('Content-Type: application/json');
try {
    // Database connection
	$database = mysqli_connect("localhost", "EoL_User", "A55ApzsgXRPS6gJb!", "camelote");
	$database->set_charset('utf8mb4');

    $id = $_POST['id'];
    //$id = 3;

    // Initialise the json array
    $resources = array();
    // Query which will get all the user's resources
    $resources_query = "SELECT resources_to_user.id, resources, resources.name, quantity FROM resources_to_user JOIN resources ON resources_to_user.resources=resources.id WHERE user_id=? ORDER BY resources";    
    // Prepare the query
    $stmt = $database->prepare($resources_query);
    // Bind the ? in the query to the $id
    $stmt->bind_param("i", $id);
    // Execute the query
    $stmt->execute();
    // Get the mysqli result
    $result = $stmt->get_result();
    
    // If no data is found then the user doesn't possess resources
    if($result->num_rows < 1) {
		echo "-1";
	} else {
        // If data is found, store it inside an array
        while($row = $result->fetch_assoc()) {
			$resources[] = $row;
		}
		// Return the array encoded in json
        echo json_encode($resources);
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