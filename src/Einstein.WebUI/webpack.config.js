const path = require('path');
const webpack = require('webpack');
const VueLoaderPlugin = require('vue-loader/lib/plugin')
const CompressionPlugin = require("compression-webpack-plugin")
const CleanWebpackPlugin = require('clean-webpack-plugin');

module.exports = (env) => {
	const isDevBuild = !(env && env.prod);

	return [{
		mode: isDevBuild ? 'development' : 'production',

		optimization: {
			minimize: !isDevBuild
		},

		stats: { modules: false },
		context: __dirname,

		entry: {
			main: './ClientApp/boot.js'
		},
		/*
	resolve: {
	  root: path.resolve('./src'),
	  extensions: ['.ts', '.tsx', '.js', 'vue' ] // '' is needed to find modules like "jquery"
	},
		*/
		output: {
			path: path.join(__dirname, 'wwwroot', 'dist'),
			publicPath: 'dist/',
			filename: "main.js"
		},

		module: {
			rules: [

				// Vue files with subloader(s)
				{
					test: /\.vue$/,
					include: /ClientApp/,
					loader: 'vue-loader'
					/*,
					options: {
						loaders: [{
							js: 'babel-loader', options: {
								"plugins": [
									["transform-es2015-spread", {
										"loose": true
									}]
								]
							}
						}]
					}*/
				},

				// Typescript (Does not work yet!)
				//{
				//	test: /\.ts?$/,
				//	exclude: '/node_modules/',
				//	include: '/ClientApp/',
				//	use: {
				//		loader: 'ts-loader',
				//		options: {
				//			appendTsSuffixTo: [/\.vue$/]
				//		}
				//	}
				//},

				// Javascript
				{
					test: /\.js$/,
					loader: 'babel-loader',
					exclude: '/node_modules/'
					/*,
					options: {
						"plugins": [
							["transform-es2015-spread", {
								"loose": true
							}]
						]
					}
					*/
				},

				// Javascript specific
				//{
				//	// if you use vue.common.js, you can remove it
				//	test: /\.esm.js$/,
				//	loader: 'babel-loader',
				//	exclude: /node_modules\/(?!vue(?!\W))/,
				//	include: [
				//		path.resolve('node_modules', 'vue/dist'),
				//	],
				//	/*
				//	options: {
				//		"plugins": [
				//			["transform-es2015-spread", {
				//				"loose": true
				//			}]
				//		]
				//	}
				//	*/
				//},



				// Image(s)
				{ test: /\.(png|jpg|jpeg|gif|svg)$/, exclude: '/node_modules/', use: 'url-loader?limit=25000' },

				// Sass / Scss styling
				{ test: /\.scss?$/, exclude: '/node_modules/', loaders: ['style-loader', 'css-loader', 'sass-loader'] },

				// CSS styling
				{
					test: /\.css$/,
					exclude: '/node_modules/',
					loader: 'style-loader!css-loader'
				},

				// Scalable Vector Graphics (SVG) files
				{ test: /\.svg(\?v=\d+\.\d+\.\d+)?$/, exclude: '/node_modules/', loader: 'file-loader?mimetype=image/svg+xml' },

				// Font files
				{
					test: /\.woff(2)?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
					exclude: '/node_modules/',
					use: [
						{
							loader: 'url-loader',
							options: {
								limit: 10000,
								mimetype: 'application/font-woff'
							}
						}
					]
				},
				{
					test: /\.(ttf|eot|svg)(\?v=[0-9]\.[0-9]\.[0-9])?$/,
					exclude: '/node_modules/',
					use: [
						{ loader: 'file-loader' }
					]
				}

				/*
				{
					test: /node_modules[\\\/]vis[\\\/].*\.js$/,
					loader: 'babel-loader',
					query: {
					cacheDirectory: true,
					presets: ["es2015"],
					plugins: [
						"transform-es3-property-literals",
						"transform-es3-member-expression-literals",
						"transform-runtime",
						["transform-es2015-spread", {
							"loose": true
						}]
					]
					}
				}
				*/
			]
		},

		plugins: [
			//new webpack.HotModuleReplacementPlugin(),
			new CleanWebpackPlugin([path.join(__dirname, 'wwwroot', 'dist') + '/main.*']),
			new VueLoaderPlugin(),
			new CompressionPlugin({
				include: path.join(__dirname, 'wwwroot', 'dist'), cache: !isDevBuild }),
			new webpack.DefinePlugin({
				'process.env': {
					NODE_ENV: JSON.stringify(isDevBuild ? 'development' : 'production')
				}
			}),
			//new webpack.DllReferencePlugin({
			//	context: __dirname,
			//	manifest: require('./wwwroot/dist/vendor-manifest.json')
			//}),
			new webpack.ProvidePlugin({
				Vue: ['vue/dist/vue.esm.js', 'default'],
				_assign: ['lodash', 'assign'],
				_chain: ['lodash', 'chain'],
				_cloneDeep: ['lodash', 'cloneDeep'],
				_forEach: ['lodash', 'forEach'],
				_includes: ['lodash', 'includes'],
				_some: ['lodash', 'some'],
				_find: ['lodash', 'find'],
				_get: ['lodash', 'get'],
				_last: ['lodash', 'last'],
				_remove: ['lodash', 'remove']
			})
		].concat(isDevBuild ? [
			// Plugins that apply in development builds only
			new webpack.SourceMapDevToolPlugin({
				filename: '[file].map', // Remove this line if you prefer inline source maps
				moduleFilenameTemplate: path.relative(path.join(__dirname, 'wwwroot', 'dist'), '[resourcePath]') // Point sourcemap entries to the original file locations on disk
			})
		] : [
				// Plugins that apply in production builds only
				//new ExtractTddextPlugin('site.css')
			]),

		resolve: {
			alias: {
				'vue$': 'vue/dist/vue.esm.js'
			}
		},




		devtool: '#eval-source-map'
	}
	];
}