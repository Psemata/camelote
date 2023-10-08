<?php
// Header necessary to tell the server to use json
header('Content-Type: application/json');
try {
    // Database connection
	$database = mysqli_connect("localhost", "EoL_User", "A55ApzsgXRPS6gJb!", "camelote");
	$database->set_charset('utf8mb4');

    /*
    $_POST[3] = 100;
    $_POST[4] = 100;
    $_POST[5] = 100;
    $_POST[6] = 100;
    $_POST[7] = 100;
    $_POST[8] = 100;
    */

    // Get all the keys, they represent which resource we need to update, the value is its new amount
    $keys = array_keys($_POST);

    // Loop on all the resources which need to be updated, 
    for($i = 0; $i < count($keys); $i++) {
        $update_query = "UPDATE resources_to_user SET quantity=? WHERE id=?";
        $stmt= $database->prepare($update_query);
        $stmt->bind_param("ii", $_POST[$keys[$i]], $keys[$i]);
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