<?php
include("bl_Common.php");
    $link=dbConnect();
 
    $name = safe($_POST['name']);
    $reason = safe($_POST['reason']);
	$m_ip = safe($_POST['myIP']);
	$m_by = safe($_POST['mBy']);
    $hash = safe($_POST['hash']);
 

    $real_hash = md5($name . $secretKey);
    if($real_hash == $hash)
    {
$check = mysql_query("SELECT * FROM BanList WHERE `name`= '$name'");
$numrows = mysql_num_rows($check);

if ($numrows == 0)
{
	
        $ins = mysql_query("INSERT INTO  `BanList` (`name` ,  `reason` ,  `myIP` ,  `mBy` ) VALUES ('".mysql_real_escape_string($name)."' ,  '".mysql_real_escape_string($reason)."' ,  '".mysql_real_escape_string($m_ip)."',  '".mysql_real_escape_string($m_by)."') ");
	
        $newstatus = 1;
        $inst = mysql_query("UPDATE MyGameDB SET status='". $newstatus ."' WHERE name= '$name'");
       
       if ($ins){
        
                die ("Done");
    }else{
		die ("Error: " . mysql_error());
        }
		}else{
		echo "This Player is already banned";
		}
		
    }else{
	echo "you do not have permission to access.";
	}
    mysql_close( $link);
?>