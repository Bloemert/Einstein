/*
 * FontAwesome 5.1 ! (Currrently a pre-release!)
 * 
 * Using SVG Icons from FontAwesome.
 */

import { config, library, dom } from '@fortawesome/fontawesome-svg-core'

config.autoReplaceSvg = 'nest';

// Default usage icons
import {
	faSpinner,
	faSearch,
	faSignInAlt,
	faSignOutAlt,
	faEye,
	faEyeSlash,
	faAngleRight,
	faAngleLeft,
	faCalendar,
	faClock,
	faCheck,
	faExclamationCircle
} from '@fortawesome/free-solid-svg-icons';

library.add(
	faSpinner,
	faSearch,
	faSignInAlt,
	faSignOutAlt,
	faEye,
	faEyeSlash,
	faAngleRight,
	faAngleLeft,
	faCalendar,
	faClock,
	faCheck,
	faExclamationCircle
);

// Icons for main page
import {
	faUniversalAccess,
	faUserCircle,
	faCaretDown
} from '@fortawesome/free-solid-svg-icons';

library.add(
	faUniversalAccess,
	faUserCircle,
	faCaretDown
);

// Icons for Master / Detail management
import {
	faHdd
} from '@fortawesome/free-regular-svg-icons';
import {
	faSyncAlt,
	faUndoAlt,
	faRedoAlt,
	faPlusSquare,
	faMinusSquare
} from '@fortawesome/free-solid-svg-icons';

library.add(
	faHdd,
	faSyncAlt,
	faUndoAlt,
	faRedoAlt,
	faPlusSquare,
	faMinusSquare
);

// Kicks off the process of finding <i> tags and replacing with <svg>
dom.watch()
