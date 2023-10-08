<?php

// Header necessary to tell the server to use json
header('Content-Type: application/json');
try {
	// Database connection
	$database = mysqli_connect("localhost", "EoL_User", "A55ApzsgXRPS6gJb!", "camelote");
	$database->set_charset('utf8mb4');

	// Value got from the POST array
	$email = $_POST["email"];
    $password = $_POST['password'];

	/*
    $email = "valentino.izzo@he-arc.ch";
    $password = "1234";
	*/
	
	// Query which will check if the user exist
	$user_exist = "SELECT id, email, password, pseudo FROM user where email=?";
	// Prepare the query
	$stmt = $database->prepare($user_exist);
	// Bind the ? in the query to the $email
	$stmt->bind_param("s", $email);
	// Execute the query
	$stmt->execute();
    // Get the mysqli result
    $result = $stmt->get_result();

	// If no data is found then the user is inexistant thus an error is sent
	if($result->num_rows < 1) {
		echo "-1";
	} else {
		// Get the user data from the email
    	$user_id_and_pswd = $result->fetch_assoc();

        if(password_verify($password, $user_id_and_pswd['password'])) {
			$user_info = $user_id_and_pswd['id'] . ',' . $user_id_and_pswd['pseudo'];
            echo $user_info;
        } else {
            echo "-1";
        }
	}

	// Close the database
	$database->close();
} catch(mysqli_sql_exception $e) {
	// Errors in case the database connection failed
	echo "MySQLi Error Code: " . $e->getCode() . "<br>";
	echo "Exception Msg: " . $e->getMessage();
	exit();
}
?>