<?php
include("bl_Common.php");

$name = strip_tags($_POST['name']);
$type = strip_tags($_POST['type']);

    $link=dbConnect();
 
 if($type == "0"){
$check = mysql_query("SELECT * FROM BanList WHERE name ='$name' ") or die(mysql_error());
$numrows = mysql_num_rows($check);

if ($numrows == 0)
{
	die ("This user does not exist in BanList! \n");
}
else
{
	while($row = mysql_fetch_assoc($check))
	{
                        echo "Exist";
                        echo "|";
                        echo $row['name'];
                        echo "|";
                        echo $row['reason'];
                        echo "|";
                        echo $row['myIP'];
                        echo "|";
                        echo $row['mBy'];
                        echo "|";
                }
}
}else if($type == "1"){

$check = mysql_query("SELECT * FROM MyGameDB WHERE name ='$name' ") or die(mysql_error());
$numrows = mysql_num_rows($check);

if ($numrows == 0)
{
	die ("This user does not exist in DataBase! \n");
}
else
{
	while($row = mysql_fetch_assoc($check))
	{
                        echo "Exist";
                        echo "|";
                        echo $row['name'];
                        echo "|";
                        echo $row['status'];
                        echo "|";
                        echo $row['uIP'];
                        echo "|";
                        echo $row['score'];
                        echo "|";
                }
}
}
mysql_close($link);
?>