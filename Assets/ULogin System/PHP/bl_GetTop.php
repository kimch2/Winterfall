<?php
include("bl_Common.php");

 $link=dbConnect();
 
        $query = "SELECT * FROM `MyGameDB` ORDER by `score` DESC LIMIT 100";
    $result = mysql_query($query) or die('Query failed: ' . mysql_error());
 
    $num_results = mysql_num_rows($result);  
 
 if($num_results > 0){
    for($i = 0; $i < $num_results; $i++)
    {
    
         $row = mysql_fetch_array($result);
         
         echo $row['name'] 
         . "|" 
         . $row['kills'] 
         . "|" 
         . $row['deaths'] 
         . "|" 
         . $row['score'] 
         . "|" 
         . $row['status'] 
         . "|\n";
         
    }
    }else{
    
    echo "There is no registered user yet!";
    
    }
    mysql_close($link);
?>