# AddEditObservation
This is a proof-of-concept project I wrote years ago just after I started at JCR.  At the time, one of our new subscription products was written in classic ASP.NET using custom controls (*.ascx). 

The product was suffering from poor performance when it tried to reder a medical survey on-screen. If the survey had too many questions, rendering the form using dynamically-created custom controls was taking way to long. My solution was to show that an alternative approach of rending mark-up from the code-behind would allow us to deliver alot of questions in a very short response time.

Download the repository and launch the web application. Now change the URL in your browser to http://localhost:4223/AddEditObservation.aspx and this is what you should see:

