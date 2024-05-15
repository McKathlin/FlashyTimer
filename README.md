# Flashy Timer, Windows Desktop Edition

This silent, highly visible timer gets your attention by flashing different colors.
If you're deaf, have ADHD, or need to watch your time closely for work, this timer might be useful for you.

## The Basics: Start, Stop, Pause, and Unpause

Pick a start button on the left to start a timer with the number of minutes indicated.
Time will start counting down. The color will change to yellow when time is low,
and will start flashing red and yellow when time is almost up.

The Stop and Pause/Resume buttons are on the right.
* The Stop button stops the timer and clears the time.
* When the timer is running, the Pause/Resume button pauses the timer.
* When the timer is paused, the Pause/Resume button makes the timer start running from its current time left.

## Advanced: Customize the Start Buttons

Each start button corresponds to a bundle of settings, as follows:
* **StartingTime**: the amount of time to put on the timer when it starts
* **WarningTime**: the amount of time left when the timer turns yellow
* **CriticalTime**: the amount of time left when the timer starts flashing red

To customize the settings on these Start buttons, open FlashyTimer's folder, find StartOptions.json,
and open it with Notepad or another text editor of your choice. You'll see each button's StartingTime, CriticalTime,
and WarningTime defined with the form `hh:mm:ss`. The file should look something like this:

    [
      	{"StartingTime":"00:05:00","WarningTime":"00:01:00","CriticalTime":"00:00:15"},
      	{"StartingTime":"00:10:00","WarningTime":"00:01:00","CriticalTime":"00:00:15"},
      	{"StartingTime":"00:30:00","WarningTime":"00:10:00","CriticalTime":"00:01:00"},
      	{"StartingTime":"00:45:00","WarningTime":"00:10:00","CriticalTime":"00:01:00"}
    ]

For example, the first start option button has a StartingTime of `"00:05:00"`, or five minutes.
Its WarningTime is `"00:01:00"`, one minute, so it turns yellow when there's one minute left.
Its CriticalTime is `"00:00:15"`, or fifteen seconds. It starts flashing red when there are 15 seconds left.
If you wanted the five minute timer to start flashing red when there are 30 seconds left, you could change the time after CriticalTime to `"00:00:30"` and save StartOptions.json with your changes. Next time Flashy Timer opens, the start options will be updated with your changes.
