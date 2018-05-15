const path = require('path');
const webpack = require('webpack');
const bundleOutputDir = './wwwroot/dist';

module.exports = (env) => {
  const isDevBuild = !(env && env.prod);

  return [{
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
	  path: __dirname + "/dist",
	  filename: "main.js",
	  publicPath: 'dist/'
	},

	module: {
	  rules: [
		{
		  test: /\.ts?$/, include: '/ClientApp/', exclude: '/node_modules/',
		  use: {
				loader: 'ts-loader',
				options: {
					appendTsSuffixTo: [/\.vue$/]
				}
		  }
		},
		{
		  test: /\.js$/,
		  loader: 'babel-loader',
		  exclude: /node_modules/,
		},
		{
		  // if you use vue.common.js, you can remove it
		  test: /\.esm.js$/,
		  loader: 'babel-loader',
		  exclude: /node_modules\/(?!vue(?!\W))/,
		  include: [
			path.resolve('node_modules', 'vue/dist'),
		  ]
		},
		{ test: /\.vue$/, include: /ClientApp/, loader: 'vue-loader', options: { loaders: [{ js: 'babel-loader' } ] } },
		{ test: /\.(png|jpg|jpeg|gif|svg)$/, use: 'url-loader?limit=25000' },
		{ test: /\.scss?$/, loaders: ['style-loader', 'css-loader', 'sass-loader'] },
			{
				test: /\.css$/,
				loader: 'style-loader!css-loader'
			},
		{ test: /\.svg(\?v=\d+\.\d+\.\d+)?$/, loader: 'file-loader?mimetype=image/svg+xml' },
		{
		  test: /\.woff(2)?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
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
		  use: [
			{ loader: 'file-loader' }
		  ]
		},
		{
		  test: /node_modules[\\\/]vis[\\\/].*\.js$/,
		  loader: 'babel-loader',
		  query: {
			cacheDirectory: true,
			presets: ["es2015"],
			plugins: [
			  "transform-es3-property-literals",
			  "transform-es3-member-expression-literals",
			  "transform-runtime"
			]
		  }
		}
	  ]
	},

	plugins: [
	  new webpack.DefinePlugin({
		'process.env': {
		  NODE_ENV: JSON.stringify(isDevBuild ? 'development' : 'production')
		}
	  }),
	  new webpack.DllReferencePlugin({
		context: __dirname,
		manifest: require('./wwwroot/dist/vendor-manifest.json')
	  }),
	  new webpack.ProvidePlugin({
		// Tether: "tether",
		// "window.Tether": "tether",
		//Vue: "vue",
		//'window.Vue': 'vue',
		//VueResource: "exports-loader?plugin!vue-resource/dist/vue-resource.esm.js",
		//Vuex: "exports-loader?index_esm!vuex/dist/vuex.esm",
		//"_": "lodash",
		//"window._": "lodash"
	  })
	].concat(isDevBuild ? [
	  // Plugins that apply in development builds only
	  new webpack.SourceMapDevToolPlugin({
		filename: '[file].map', // Remove this line if you prefer inline source maps
		moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]') // Point sourcemap entries to the original file locations on disk
	  })
	] : [
		// Plugins that apply in production builds only
		new webpack.optimize.UglifyJsPlugin(),
		new ExtractTextPlugin('site.css')
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