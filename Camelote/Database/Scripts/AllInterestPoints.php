<?php
// Header necessary to tell the server to use json
header('Content-Type: application/json'); 
try {
	// Database connection
	$database = mysqli_connect("localhost", "EoL_User", "A55ApzsgXRPS6gJb!", "camelote");
	$database->set_charset('utf8mb4');

	// Initialise the json array
	$poi = array();
	// Query which will get all the interest_points
	$poi_query = "SELECT * FROM interest_point";
	// Get the data from the query
	if ($result = $database->query($poi_query)) {
		// If data is found, store it inside an array
		while($row = $result->fetch_assoc()) {
			$poi[] = $row;
		}
		// Return the array encoded in json
		echo json_encode($poi);
	} else {
		// No point of interests, send 1 as an error
		echo "-1";
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
