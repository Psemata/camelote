<?php
// Header necessary to tell the server to use json
header('Content-Type: application/json');
try {
    // Database connection
	$database = mysqli_connect("localhost", "EoL_User", "A55ApzsgXRPS6gJb!", "camelote");
	$database->set_charset('utf8mb4');

    $id = $_POST['id'];
    //$id = 3;

    // array used to stock the castle, needed to format the end in the unity script
    $castle_array = array();

    // Query which will get the user's castle
    $castle_query = "SELECT id, level, start_time, end_time FROM castle WHERE owner=?";    
    // Prepare the query
    $stmt = $database->prepare($castle_query);
    // Bind the ? in the query to the $id
    $stmt->bind_param("i", $id);
    // Execute the query
    $stmt->execute();
    // Get the mysqli result
    $result = $stmt->get_result();
    
    // If no data is found then the user doesn't have a castle
    if($result->num_rows < 1) {
		echo "-1";
	} else {
       $castle_array[] = $result->fetch_assoc();       
       echo json_encode($castle_array);
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