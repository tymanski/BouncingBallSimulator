// Circle class
var Circle = function (id, ctx, x, y, dx, dy, radius, drawColor) {
    this.id = id;
    this.ctx = ctx;
    this.x = x;
    this.y = y;
    this.dx = dx;
    this.dy = dy;
    this.radius = radius;
    this.drawColor = drawColor;

    this.drawBall = function () {
        this.ctx.beginPath();
        this.ctx.arc(this.x, this.y, this.radius, 0, Math.PI * 2);
        this.ctx.fillStyle = this.drawColor;
        this.ctx.lineWidth = 1;
        this.ctx.strokeStyle = '#0666';
        this.ctx.stroke();
        this.ctx.fill();
        this.ctx.closePath();
    }

    this.move = function () {
        this.x += this.dx;
        this.y += this.dy;
    }

    this.moveBack = function () {
        this.x -= this.dx;
        this.y -= this.dy;
    }
};

var GameManager = function () {
    this.canvas = document.getElementById("viewport");
    this.ctx = this.canvas.getContext("2d");
    this.ballRadius = 15;
    this.x = this.canvas.width / 2;
    this.y = this.canvas.height - 50;
    this.speed = 1;

    this.circles = [];

    this.addRandomCircle = function (obj) {
        var retryCount = 10;
        
        while (retryCount > 0) {
            x = getRandomInt(0 + this.ballRadius, this.canvas.width - this.ballRadius);
            y = getRandomInt(0 + this.ballRadius, this.canvas.height - this.ballRadius);

            var newBall = new Circle(obj.Id, this.ctx, x, y, this.speed * Math.random(), this.speed * Math.random(), this.ballRadius, obj.DrawColor)

            var isCollision = false;

            for (var i = 0; i < this.circles.length; i++) {
                if (this.checkBallsCollision(newBall, this.circles[i])) {
                    isCollision = true;
                }
            }

            if (!isCollision) {
                this.circles.push(newBall);
                retryCount = 0;
            }

            retryCount--;
        }
    }

    this.MoveBall = function (i) {
        ball = this.circles[i];

        // Wall hits
        if (ball.x + ball.dx > this.canvas.width - ball.radius || ball.x + ball.dx < ball.radius) {
            ball.dx = -ball.dx;
        }
        if (ball.y + ball.dy > this.canvas.height - ball.radius || ball.y + ball.dy < ball.radius) {
            ball.dy = -ball.dy;
        }

        ball.move();
    }

    this.draw = function (current) {
        ball = this.circles[current];
        
        var newX, newY;

	    // iterate all already rendered objects
	    // if collision, set new speed for both
        for (var i = 0; i < current; i++) {

            // If collision
            if (this.checkBallsCollision(ball, this.circles[i])) {
               
                this.circles[i].moveBack();
                ball.moveBack();

                oldx = this.circles[i].dx;
                oldy = this.circles[i].dy;

                this.circles[i].dx = ball.dx;
                this.circles[i].dy = ball.dy;

                ball.dx = oldx;
                ball.dy = oldy;
            }
        }

        ball.drawBall();
    }

    this.checkBallsCollision = function (ball1, ball2) {
        // Get distance between balls
        var d = Math.sqrt((ball1.x - ball2.x) * (ball1.x - ball2.x) + (ball1.y - ball2.y) * (ball1.y - ball2.y));

        return (ball2.radius + ball1.radius) >= d ? true : false;
    }

    this.drawAll = function () {
        this.ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);

        // First move all balls
        for (var i = 0; i < this.circles.length; i++) {
            this.MoveBall(i);
        }

        // Then check for collisons and draw
        for (var i = 0; i < this.circles.length; i++) {
            this.draw(i);
        }
    }

    this.updatePlayer = function (player) {
        var result = this.circles.find(x => { return x.id == player.Id });
        result.drawColor = player.DrawColor;
    }

    this.addPlayer = function (player) {
        this.addRandomCircle(player);
    }

    this.removePlayer = function (id) {
        var index = this.circles.findIndex(x => { return x.id == id });
        if (index > -1) {
            this.circles.splice(index, 1);
        }
    }
}

function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}