<?php
// Header necessary to tell the server to use json
header('Content-Type: application/json');
try {
    // Database connection
	$database = mysqli_connect("localhost", "EoL_User", "A55ApzsgXRPS6gJb!", "camelote");
	$database->set_charset('utf8mb4');

    // Get the user id and the castle's new level
    $id = $_POST['id'];
    $level = $_POST['level'];

    // Loop on all the resources which need to be updated, 
    $update_query = "UPDATE castle SET level=? WHERE owner=?";
    $stmt= $database->prepare($update_query);
    $stmt->bind_param("ii", $level, $id);
    $stmt->execute();

    // Close the database
	$database->close();
} catch (mysqli_sql_exception $e) { 
	// Errors in case the database connection failed
	echo "MySQLi Error Code: " . $e->getCode() . "<br />";
	echo "Exception Msg: " . $e->getMessage();
	exit(); 
}
?>
