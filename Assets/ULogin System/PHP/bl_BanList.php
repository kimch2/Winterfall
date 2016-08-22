<?php
include("bl_Common.php");

 $link=dbConnect();
 
        $query = "SELECT * FROM `BanList` ORDER by `id` DESC";
    $result = mysql_query($query) or die('Query failed: ' . mysql_error());
 
    $num_results = mysql_num_rows($result);  
 if($num_results > 0){
    for($i = 0; $i < $num_results; $i++)
    {
         $row = mysql_fetch_array($result);
         echo $row['name'] . "|" . $row['reason'] . "|" . $row['myIP'] . "|" . $row['mBy']. "|\n";
    }
	}else{
	echo "Emty";
	}
    mysql_close($link);
?>