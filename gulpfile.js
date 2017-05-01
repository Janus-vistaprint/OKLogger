const {restore, build, publish, pack, push} = require('gulp-dotnet-cli');
const configuration = 'Release';
const version = '1.1.' + ( process.env.BUILD_NUMBER  || '3');
const download = require('gulp-download');
const decompress = require('gulp-decompress');
const path = require('path');
const gulp = require('gulp');
const http = require('http');
const fs = require('fs');
const del = require('del');
const cp = require('child_process');
const os = require('os');
const spawn = require('child-process-promise').spawn;
const exec = require('child-process-promise').exec;

console.log("version => " + version);

gulp.task('clean', ()=>del(['**/bin', '**/obj', 'output', 'outputs', '**/nuget-package']));

gulp.task('restore', ['clean'], ()=>
    gulp.src('**/*.csproj')
        .pipe(restore())
          
);

gulp.task('build', ['restore'], ()=>
    gulp.src('**/*.csproj', {read:false})
        .pipe(build(
            {
                configuration: configuration,
                version: version 
            }))
);

gulp.task('publish', ['restore'], ()=>
    gulp.src('src/Gallery.PIMDataService/Gallery.PIMDataService.csproj')
        .pipe(publish( 
            {
                configuration: configuration,
                version: version,
                output:  path.join(process.cwd(), 'output')
            }))
);

gulp.task('buildNugetPackage', ['build'], () => {
    console.log("version:" + version);
    return gulp.src('src/OKLogger/OKLogger.csproj')
        .pipe(pack(
            {
                output: "nuget-package",
                includeSymbols: false,
                configuration: configuration,
                version: version,
                noBuild: true
            }));
});


gulp.task('publishNugetPackage', [], () => {
    return gulp.src('src/OKLogger/nuget-package/*.nupkg')
        .pipe(push(
            {
                source: `http://${process.env.ARTIFACT_SERVER}/api/nuget/nuget-release-local`,
                apiKey: process.env.NUGET_API_KEY
            }));
});

/* grab aws keys from zanzibar */
gulp.task('zanzibar:install', ()=> spawn('gem', ['install', 'zanzibar'], {stdio:'inherit'}) );

gulp.task('zanzibar:Bundle', ['zanzibar:install'], ()=> spawn('zanzibar', ['bundle'], {stdio:'inherit'}) );

gulp.task('zanzibar:loadAwsKeys', ['zanzibar:Bundle'], () => {
    var secret = require('./secrets/keys.json');
    process.env["AWS_ACCESS_KEY_ID"] = secret["AWSAccessKey"];
    process.env["AWS_SECRET_ACCESS_KEY"] = secret["AWSSecretKey"];
    
});