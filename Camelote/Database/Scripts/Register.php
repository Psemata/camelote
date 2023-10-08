<?php
// Header necessary to tell the server to use json
header('Content-Type: application/json');
try {
	// Database connection
	$database = mysqli_connect("localhost", "EoL_User", "A55ApzsgXRPS6gJb!", "camelote");
	$database->set_charset('utf8mb4');

	// Value got from the POST array
	$pseudo = $_POST["pseudo"];
	$surname = $_POST["surname"];
	$name = $_POST["name"];
	$email = $_POST["email"];
	$password = password_hash($_POST['password'], PASSWORD_DEFAULT);
	
	/*
	$pseudo = "testUser";
	$surname = "testUser";
	$name = "testUser";
	$email = "testUser.izzo@he-arc.ch";
	$password = password_hash("1234", PASSWORD_DEFAULT);
	*/

	// Query which will check if the user exist
	$user_exist = "SELECT id FROM user where email=?";
	// Prepare the query
	$stmt = $database->prepare($user_exist);
	// Bind the ? in the query to the $email
	$stmt->bind_param("s", $email);
	// Execute the query
	$stmt->execute();
	// If no data is found then, the user is getting registered
	if($stmt->get_result()->num_rows < 1) {
		// Creation of the user
		$register_query = "INSERT INTO user (pseudo, surname, name, email, password) VALUES (?, ?, ?, ?, ?)";
		$stmt = $database->prepare($register_query);
		$stmt->bind_param("sssss", $pseudo, $surname, $name, $email, $password);
		$stmt->execute();

		// Get the user id
		$user_id = "SELECT id FROM user where email=?";
		$stmt = $database->prepare($user_id);
		$stmt->bind_param("s", $email);
		$stmt->execute();
		$result = $stmt->get_result();
		$id = $result->fetch_assoc()["id"];
		
		// Create the castle for the new user
		$new_castle_query = "INSERT INTO castle (level, latitude, longitude, start_time, end_time, owner) VALUES (?, ?, ?, ?, ?, ?)";
		$stmt = $database->prepare($new_castle_query);
		$level = 1;
		$other = 0;
		$stmt->bind_param("iiiiii", $level, $other, $other, $other, $other, $id);
		$stmt->execute();
	} else {
		// Otherwise, the email is already used and 1 is sent as an error
		echo "-1";
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
