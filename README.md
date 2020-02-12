# CheckMoodleApp

Simple app which makes requests to the Moodle API and triggers an alarm when the contents of a selected course have changed.

This is aimed at students from universities which use the Moodle platform, who are waiting for important course updates - like the upload of exam results, which is what inspired this app :)

The user signs in with their credentials, then the app pulls the list of courses they are enrolled in based on the user id. The user then chooses one of these courses, and the app starts continuously making requests to get the contents of the course. When a change in the contents is detected (e.g. a new message was posted on the forum or a file was uploaded), the app sounds an alarm to alert the user.

Currently the app is using the "https://acs.curs.pub.ro/2019" Moodle (Faculty of Automatic Control and Computers, specialization of Systems Engineering from the Politehnica University of Bucharest) to make requests, but this is easily changeable in the ApiCallHandler.
