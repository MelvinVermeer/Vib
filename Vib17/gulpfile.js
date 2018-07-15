var gulp = require('gulp');
var sass = require('gulp-sass');
var autoprefixer = require('gulp-autoprefixer');
var cleanCss = require('gulp-clean-css');
var rename = require('gulp-rename');
var sourcemaps = require('gulp-sourcemaps');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var livereload = require('gulp-livereload');
var cssShorthand = require('gulp-shorthand');
var babel = require('gulp-babel');

settings = {};
settings.watchForSassFiles = "wwwroot/src/*.scss"; // Deze files worden in de gaten gehouden voor changes
settings.mainSrcSassFile = "wwwroot/src/style.scss"; // Deze file wordt gebruikt voor de compilatie
settings.buildCssTargetDirectory = "wwwroot/css"; // Hier wordt de gecompileerde css naartoe geschreven


gulp.task('default', ['do-sass']);

gulp.task('do-sass', function () {
    return gulp.src(settings.mainSrcSassFile)
        .pipe(sourcemaps.init())
        .pipe(sass({ outputStyle: 'nested' }).on('error', function (err) { console.log(err); this.emit('end') }))
        .pipe(autoprefixer())
        .pipe(cssShorthand())
        .pipe(gulp.dest(settings.buildCssTargetDirectory))
        .pipe(cleanCss())
        .pipe(rename({ extname: '.min.css' }))
        .pipe(sourcemaps.write(""))
        .pipe(gulp.dest(settings.buildCssTargetDirectory))
});

settings.mainSrcJsFile = "wwwroot/js/site.js"; // Deze files worden in de gaten gehouden voor changes
settings.buildJsTargetDirectory = "wwwroot/js"; // Hier wordt de gecompileerde js naartoe geschreven

gulp.task('do-js', function () {
    return gulp.src(settings.mainSrcJsFile)
        .pipe(sourcemaps.init())
        .pipe(gulp.dest(settings.buildJsTargetDirectory))
        .pipe(rename({ extname: '.es5.min.js' }))
        .pipe(babel({ presets: ['es2015'] }))
        //.pipe(uglify())
        .pipe(sourcemaps.write(''))
        .pipe(gulp.dest(settings.buildJsTargetDirectory));
});

gulp.task('watch', function () {
    livereload.listen();

    gulp.watch(settings.watchForSassFiles, ['do-sass']);

    // reload stuff
    gulp.watch([
        settings.buildCssTargetDirectory + '/*.css',
    ], function (obj) {
        if (obj.type === 'changed') {
            return gulp.src(obj.path).pipe(livereload());
        }
    });
});
